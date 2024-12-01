use std::collections::HashSet;

fn main() {
    let input = include_bytes!("input/day04");
    let mut card_amounts: Vec<usize> = vec![1; input.iter().filter(|&&b| b == b'\n').count() + 1];

    println!(
        "Solution 4a: {}",
        input
            .split(|&b| b == b'\n')
            .enumerate()
            .map(|(i, line)| {
                if let [_, numbers] = line.split(|&b| b == b':').collect::<Vec<_>>().as_slice() {
                    if let [winning_numbers, your_numbers] = numbers.split(|&b| b == b'|').collect::<Vec<_>>().as_slice() {
                        
                        let wins = to_num_set(your_numbers)
                            .intersection(&to_num_set(winning_numbers))
                            .count();
        
                        (1..wins + 1)
                            .for_each(|n| {
                                if i + n < card_amounts.len() {
                                    card_amounts[i + n] += 1 * card_amounts[i];
                                }
                            });
                        
                        card_amounts[i]
                    } else {
                        panic!("Invalid Input");
                    }
                } else {
                    panic!("Invalid Input");
                }
            })
            .sum::<usize>()
    );
}

fn to_num_set(numbers: &[u8]) -> HashSet<usize> {
    numbers
        .split(|&b| b == b' ')  
        .filter(|num| num.iter().any(|&b| b.is_ascii_digit()))
        .map(|num| parse_number(num))
        .collect::<HashSet<_>>()
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
