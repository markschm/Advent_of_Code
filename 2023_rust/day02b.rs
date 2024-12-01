use std::cmp;

fn main() {
    println!(
        "Solution 2b: {}",
        include_bytes!("input/day02")
            .split(|&b| b == b'\n')
            .map(|line| {
                let split_on_colon: Vec<_> = line
                    .split(|&b| b == b':')
                    .collect();

                if let [_, game_rounds] = split_on_colon.as_slice() {
                    let max_color_tuple = game_rounds
                        .split(|&b| b == b';')
                        .flat_map(|round| round.split(|&b| b == b','))
                        .fold((0, 0, 0), |mut acc, group| {
                            match get_color_index(group) {
                                0 => acc.0 = cmp::max(acc.0, parse_number(group)),
                                1 => acc.1 = cmp::max(acc.1, parse_number(group)),
                                2 => acc.2 = cmp::max(acc.2, parse_number(group)),
                                _ => todo!(),
                            }

                            acc
                        });

                    max_color_tuple.0 * max_color_tuple.1 * max_color_tuple.2

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

fn get_color_index(group: &[u8]) -> usize {
    if group.windows(3).position(|window| window == b"red").is_some() {
        0
    } else if group.windows(5).position(|window| window == b"green").is_some() {
        1
    } else {
        2
    }
}
