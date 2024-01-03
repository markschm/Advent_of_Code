fn main() {
    let grid: Vec<Vec<u8>> = include_bytes!("input/day03")
        .split(|&b| b == b'\n')
        .map(|line| line.to_vec())
        .collect();

    let width = grid[0].len() - 1;
    let height = grid.len();

    let mut res = 0;

    for y in 0..height {
        for x in 0..width {
            if grid[y][x].is_ascii_digit() && (x == 0 || !grid[y][x - 1].is_ascii_digit()) {
                let mut num_length = 0;

                while x + num_length < width && grid[y][x + num_length].is_ascii_digit() {
                    num_length += 1;
                }

                if has_adjacent_symbol(&grid, y, x, num_length) {
                    let n = parse_number(&grid[y][x..x + num_length]);
                    res += n;
                }
            }
        }
    }

    println!("Solution 3a: {}", res);
}

fn has_adjacent_symbol(grid: &Vec<Vec<u8>>, y: usize, x: usize, num_length: usize) -> bool {
    let mut adjacents: Vec<(usize, usize)> = Vec::new();

    if x > 0 {
        if y > 0 {
            adjacents.push((x - 1, y - 1));
        }

        adjacents.push((x - 1, y));
        adjacents.push((x - 1, y + 1));
    }
    adjacents.push((x + num_length, y));

    if y > 0 {
        adjacents.extend(
            (0..num_length + 1).map(|i| (x + i, y - 1)).collect::<Vec<_>>()
        );
    }

    adjacents.extend(
        (0..num_length + 1).map(|i| (x + i, y + 1)).collect::<Vec<_>>()
    );

    adjacents.retain(|&(px, py)| px < grid[0].len() && py < grid.len());

    adjacents
        .iter()
        .any(|&(px, py)| {
            grid[py][px].is_ascii_punctuation() && grid[py][px] != b'.'
        })
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
