const NUMBERS: [&[u8]; 10] = [
    b"zero", b"one", b"two", b"three", b"four", b"five", b"six", b"seven", b"eight", b"nine",
];

fn main() {
    println!(
        "Solution 1b: {}",
        include_bytes!("input/day01")
            .split(|&b| b == b'\n')
            .map(|line| {
                (
                    (0..line.len()).find_map(|i| find_first_num(line, i)).unwrap() * 10
                  + (0..line.len() - 1).rev().find_map(|i| find_first_num(line, i)).unwrap()
                ) as usize
            })
            .sum::<usize>()
    );
}

fn find_first_num(line: &[u8], i: usize) -> Option<usize> {
    line[i].is_ascii_digit()
        .then_some((line[i] - b'0') as usize)
        .or(
            NUMBERS.iter().enumerate()
               .find(|(_, num)| line[i..].starts_with(num))
               .map(|(index, _)| index) 
        )
}
