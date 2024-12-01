fn main() {
    let grid: Vec<Vec<u8>> = include_bytes!("input/day03")
        .split(|&b| b == b'\n')
        .map(|line| line.to_vec())
        .collect();

    let width = grid[0].len() - 1;
    let height = grid.len();

    println!(
        "Solution 3a: {}",
        (0..height)
            .flat_map(|y| (0..width).map(move |x| (x, y)))
            .filter(|&(x, y)| {
                grid[y][x].is_ascii_digit() && (x == 0 || !grid[y][x - 1].is_ascii_digit())
            })
            .map(|(x, y)| {
                let num_length = grid[y][x..]
                    .iter()
                    .take_while(|b| b.is_ascii_digit())
                    .count();

                (x, y, num_length)
            })
            .filter(|&(x, y, num_length)| has_adjacent_symbol(&grid, y, x, num_length))
            .map(|(x, y, num_length)| parse_number(&grid[y][x..x + num_length]))
            .sum::<usize>()
    );
}

fn has_adjacent_symbol(grid: &Vec<Vec<u8>>, y: usize, x: usize, num_length: usize) -> bool {
    let mut adjacents: Vec<(usize, usize)> = Vec::new();

    adjacents.push((x + num_length, y));
    adjacents.extend((0..3).map(|i| (x.saturating_sub(1), (y + 1).saturating_sub(i))).collect::<Vec<_>>());
    adjacents.extend((0..num_length + 1).map(|i| (x + i, y.saturating_sub(1))).collect::<Vec<_>>());
    adjacents.extend((0..num_length + 1).map(|i| (x + i, y + 1)).collect::<Vec<_>>());

    adjacents
        .iter()
        .filter(|&(px, py)| px < &grid[0].len() && py < &grid.len())
        .any(|&(px, py)| grid[py][px].is_ascii_punctuation() && grid[py][px] != b'.')
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
