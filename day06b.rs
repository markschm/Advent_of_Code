fn main() {
    let mut split_input = include_bytes!("input/day06").split(|&b| b == b'\n');
    let time = parse_line(split_input.next().unwrap());
    let distance = parse_line(split_input.next().unwrap());
    
    println!(
        "Solution 6b: {}",
        (0..)
            .find(|&d| {
                let speed = (time / 2).saturating_sub(d);
                let race_time = time.saturating_sub(speed);

                speed * race_time <= distance
            })
            .map_or(1, |d| (d * 2).saturating_sub((time + 1) % 2))
    );
}

fn parse_line(line: &[u8]) -> usize {
    line
        .split(|&b| b == b':')
        .map(|num| parse_number(num))
        .filter(|&num| num > 0)
        .next()
        .unwrap()
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
