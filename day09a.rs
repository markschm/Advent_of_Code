fn main() {
    println!(
        "Solution 9a: {}",
        include_bytes!("input/day09")
            .split(|&b| b == b'\n')
            .map(|line| get_nums_from_byte_arr(line))
            .map(|history| {
                let mut all_differences = vec![history];

                loop {
                    let last_index = all_differences.len() - 1;
                    let next_differences = all_differences[last_index]
                        .windows(2)
                        .map(|window| window[1] - window[0])
                        .collect();

                    all_differences.push(next_differences);
                    if all_differences[last_index + 1].iter().all(|&val| val == 0) {
                        break;
                    }
                }
                
                for i in (1..all_differences.len()).rev() {
                    let lower_val = all_differences[i][all_differences[i].len() - 1];
                    let higher_val = all_differences[i - 1][all_differences[i - 1].len() - 1];

                    all_differences[i - 1].push(lower_val + higher_val);
                }

                all_differences[0][all_differences[0].len() - 1]
            })
            .sum::<isize>()
    );
}

fn get_nums_from_byte_arr(bytes: &[u8]) -> Vec<isize> {
    bytes
        .split(|&b| b.is_ascii_whitespace())
        .map(|num| {
            let mut sign = 1;
            let mut val = 0;

            for b in num {
                if b.is_ascii_digit() {
                    val *= 10;
                    val += (b - b'0') as isize;
                } else {
                    sign = -1;
                }
            }

            sign * val
        })
        .collect::<Vec<_>>()
}
