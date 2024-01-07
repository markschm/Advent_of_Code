use std::collections::HashMap;
use std::cmp::Ordering;

#[derive(PartialEq, Eq, PartialOrd, Ord)]
enum HandType { HighCard, OnePair, TwoPair, ThreeOfKind, FullHouse, FourOfKind, FiveOfKind }

struct Hand {
    hand: [usize; 5],
    bid: usize,
    hand_type: HandType,
}

fn main() {
    println!(
        "Solution 7b: {}",
        build_ordered_hands(include_bytes!("input/day07"))
            .iter()
            .enumerate()
            .map(|(i, hand)| (i + 1) * hand.bid)
            .sum::<usize>()
    );
}

fn build_ordered_hands(bytes: &[u8]) -> Vec<Hand> {
    let card_map = build_card_map();

    let mut vec = bytes
        .split(|&b| b == b'\n')
        .map(|line| {
            if let [hand, bid] = line
                .split(|&b| b == b' ')
                .collect::<Vec<_>>()
                .as_slice() {
                
                Hand {
                    hand: {
                        let mut arr = [0; 5];
                        for i in 0..arr.len() {
                            arr[i] = *card_map.get(&hand[i]).unwrap();
                        }

                        arr
                    },
                    bid: parse_number(bid),
                    hand_type: compute_hand_type(*hand, &card_map),
                }
            } else { panic!("fail") }
        })
        .collect::<Vec<Hand>>();
         
    vec.sort_by(|a, b| compare_hands(a, b));
    vec
}

fn compare_hands(a: &Hand, b: &Hand) -> Ordering {
    if a.hand_type != b.hand_type {
        a.hand_type.cmp(&b.hand_type)
    } else {
        for i in 0..a.hand.len() {
            if a.hand[i] != b.hand[i] {
                return a.hand[i].cmp(&b.hand[i]);
            }
        }
        a.bid.cmp(&b.bid)
    }
}

fn compute_hand_type(bytes: &[u8], card_map: &HashMap<u8, usize>) -> HandType {
    card_map
        .keys()
        .map(|card_byte| {
            let card_counts: HashMap<u8, usize> = bytes
                .iter()
                .fold(HashMap::new(), |mut count_map, &b| {
                    *count_map.entry(if b == b'J' { *card_byte } else { b }).or_insert(0) += 1;
                    count_map
                });
        
            let mut total_value = 0;
            for &value in card_counts.values() {
                match value {
                    5 => return HandType::FiveOfKind,
                    4 => return HandType::FourOfKind,
                    2 | 3 => total_value += value,
                    _ => {}
                }
            }
        
            match total_value {
                5 => HandType::FullHouse,
                3 => HandType::ThreeOfKind,
                4 => HandType::TwoPair,
                2 => HandType::OnePair,
                _ => HandType::HighCard,
            }
        })
        .max()
        .unwrap()
}

// sadly couldn't find a better way to do this so this
// terrible function exists
fn build_card_map() -> HashMap<u8, usize> {
    let mut card_map = HashMap::new();
    card_map.insert(b'A', 12);
    card_map.insert(b'K', 11);
    card_map.insert(b'Q', 10);
    card_map.insert(b'T', 9);
    card_map.insert(b'9', 8);
    card_map.insert(b'8', 7);
    card_map.insert(b'7', 6);
    card_map.insert(b'6', 5);
    card_map.insert(b'5', 4);
    card_map.insert(b'4', 3);
    card_map.insert(b'3', 2);
    card_map.insert(b'2', 1);
    card_map.insert(b'J', 0);

    card_map
}

// copied over from day02
fn parse_number(bytes: &[u8]) -> usize {
    bytes
        .iter()
        .fold(0, |acc, &b| {
            if b.is_ascii_digit() {
                acc * 10 + (b - b'0') as usize
            } else {
                acc
            }
        })
}
