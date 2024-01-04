struct MapData {
    destination_index: isize,
    offset: isize,
    range_len: isize,
}

const MAP_NUM: usize = 7;

// takes forever to run, come back later and fix
fn main() {
    let input = include_bytes!("input/day05");
    let maps: Vec<Vec<MapData>> = build_maps(input);
    let seeds: Vec<usize> = build_seed_vector(input);

    println!(
        "Solution 5b: {}",
        (0..isize::MAX)
            .find(|&i| {
                let seed = maps
                    .iter()
                    .rev()
                    .fold(i as isize, |acc, map| {
                        map
                            .iter()
                            .find(|data| acc >= data.destination_index && acc < data.destination_index + data.range_len)
                            .map(|data| acc + data.offset)
                            .unwrap_or(acc)
                    });

                seeds.contains(&(seed as usize))
            })
            .unwrap()
    );
}

fn build_maps(input: &[u8]) -> Vec<Vec<MapData>> {
    let mut map_start_line = input
        .split(|&b| b == b'\n')
        .skip(2);

    (0..MAP_NUM)
        .map(|_| {
            (&mut map_start_line)
                .skip(1)
                .take_while(|line| line.iter().find(|&b| b.is_ascii_digit()).is_some())
                .map(|line| {
                    let nums: Vec<_> = line
                        .split(|&b| b == b' ')
                        .map(|num| parse_number(num) as isize)
                        .collect();

                    MapData {
                        destination_index: nums[1],
                        offset: nums[1] - nums[0],
                        range_len: nums[2],
                    }
                })
                .collect()
        })
        .collect()
}

fn build_seed_vector(input: &[u8]) -> Vec<usize> {
    input
        .split(|&b| b == b'\n')
        .next()
        .and_then(|line| line.split(|&b| b == b':').skip(1).next())
        .and_then(|numbers| Some (
            numbers
                .split(|&b| !b.is_ascii_digit())
                .map(|seed| parse_number(seed))
                .filter(|&seed| seed > 0)
                .collect::<Vec<_>>()
        ))
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
