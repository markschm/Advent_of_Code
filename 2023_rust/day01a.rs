fn main() {
    println!(
        "Solution 1a: {}",
        include_bytes!("input/day01")
            .split(|&b| b == b'\n')
            .map(|line| {
                (
                    (line.iter()
                        .find(|&b| b.is_ascii_digit())
                        .unwrap()
                        - b'0') * 10
                  + (line.iter()
                        .rev()
                        .find(|&b| b.is_ascii_digit())
                        .unwrap() 
                        - b'0')
                ) as usize
            })
            .sum::<usize>()
    );
}
