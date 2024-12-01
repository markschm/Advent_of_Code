use std::collections::HashSet;

fn main() {
    let grid: Vec<Vec<u8>> = include_bytes!("input/day03")
        .split(|&b| b == b'\n')
        .map(|line| line.to_vec())
        .collect();

    let width = grid[0].len() - 1;
    let height = grid.len();

    println!(
        "Solution 3b: {}",
        (0..height)
            .flat_map(|y| (0..width).map(move |x| (x, y)))
            .filter(|&(x, y)| grid[y][x] == b'*')
            .map(|(x, y)| {
                get_adjacent_cells(&grid, y, x, 1)
                    .iter()
                    .filter(|(x, y)| grid[*y][*x].is_ascii_digit())
                    .map(|&(x, y)| find_number(&grid[y], x))
                    .collect::<HashSet<_>>()
            })
            .filter(|nums| nums.len() == 2)
            .map(|nums| nums.iter().fold(1, |acc, num| acc * num))
            .sum::<usize>()
    );
}

fn get_adjacent_cells(grid: &Vec<Vec<u8>>, y: usize, x: usize, num_length: usize) -> Vec<(usize, usize)> {
    let mut adjacents: Vec<(usize, usize)> = Vec::new();

    adjacents.push((x + num_length, y));
    adjacents.extend((0..3).map(|i| (x.saturating_sub(1), (y + 1).saturating_sub(i))).collect::<Vec<_>>());
    adjacents.extend((0..num_length + 1).map(|i| (x + i, y.saturating_sub(1))).collect::<Vec<_>>());
    adjacents.extend((0..num_length + 1).map(|i| (x + i, y + 1)).collect::<Vec<_>>());

    adjacents.retain(|&(px, py)| px < grid[0].len() && py < grid.len());

    adjacents
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

fn find_number(bytes: &Vec<u8>, index: usize) -> usize {
    let mut num_start = index;
    while num_start > 0 && bytes[num_start].is_ascii_digit() {
        num_start -= 1;
    }
    
    let mut num_end = index;
    while num_end < bytes.len() && bytes[num_end].is_ascii_digit() {
        num_end += 1;
    }

    parse_number(&bytes[num_start..num_end])
}
