use std::collections::HashSet;

fn main() {
    println!(
        "Solution 4a: {}",
        include_bytes!("input/day04")
            .split(|&b| b == b'\n')
            .map(|line| {
                // maybe find better way to do this
                if let [_, numbers] = line.split(|&b| b == b':').collect::<Vec<_>>().as_slice() {
                    if let [winning_numbers, your_numbers] = numbers.split(|&b| b == b'|').collect::<Vec<_>>().as_slice() {
                        let winning_set = winning_numbers
                            .split(|&b| b == b' ')
                            .collect::<HashSet<_>>();

                        let your_set = your_numbers
                            .split(|&b| b == b' ')
                            .collect::<HashSet<_>>();

                        let mut wins = 0;
                        for num in your_set {
                            // move to filter later
                            if num.len() == 0 {
                                continue
                            }

                            if winning_set.contains(num) {
                                wins += 1;
                            }
                        }

                        if wins == 0 {
                            0
                        } else {
                            usize::pow(2, wins - 1)
                        }
                    } else {
                        panic!("Invalid input");
                    }
                } else {
                    panic!("Invalid input");
                }
            })
            .sum::<usize>()
    );
}
