const TOTAL_RED: usize = 12;
const TOTAL_GREEN: usize = 13;
const TOTAL_BLUE: usize = 14;

fn main() {
    println!(
        "Solution 2a: {}",
        include_bytes!("input/day02")
            .split(|&b| b == b'\n')
            .map(|line| {
                let split_on_colon: Vec<_> = line
                    .split(|&b| b == b':')
                    .collect();

                if let [game_title, game_rounds] = split_on_colon.as_slice() {
                    let first_invalid_hand = game_rounds
                        .split(|&b| b == b';')
                        .flat_map(|round| round.split(|&b| b == b','))
                        .map(|group| is_valid_hand(group))
                        .find(|&is_valid| !is_valid);

                    if first_invalid_hand.is_some() {
                        return 0
                    } 
                    return parse_number(game_title)

                } else {
                    panic!("Invalid Input");
                }
            })
            .sum::<usize>()
    );
}

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

fn is_valid_hand(group: &[u8]) -> bool {
    let cubes = parse_number(group);

    if group.windows(3).position(|window| window == b"red").is_some() {
        TOTAL_RED >= cubes
    } else if group.windows(5).position(|window| window == b"green").is_some() {
        TOTAL_GREEN >= cubes
    } else {
        TOTAL_BLUE >= cubes
    }
}
