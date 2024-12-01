use std::collections::HashMap;

// Original solution was taking too long, this solution was created after 
// watching a video about using the lcm method to solve the problem for part 2
fn main() {
    let input = include_bytes!("input/day08");
    let pattern = build_pattern(input); 
    let node_map = build_node_map(input);

    println!(
        "Solution 8b: {}",
        node_map
            .iter()
            .filter(|(key, _)| key[2] == b'A')
            .map(|(key, _)| {
                let mut i = 0;
                let mut curr_key = convert_to_fixed_array(&key[0..3]);

                loop {
                    curr_key = node_map.get(&curr_key).unwrap()[pattern[i % pattern.len()]];
                    i += 1;
        
                    if curr_key[2] == b'Z' {
                        return i;
                    }
                }
            })
            .fold(1, |acc, path_length| lcm(acc, path_length))
    );
}

fn build_pattern(bytes: &[u8]) -> Vec<usize> {
    bytes
        .split(|&b| b == b'\n')
        .next()
        .unwrap()
        .iter()
        .map(|&b| if b == b'L' { 0 } else { 1 })
        .collect::<Vec<_>>()
}

fn build_node_map(bytes: &[u8]) -> HashMap<[u8; 3], Vec<[u8; 3]>> {
    bytes
        .split(|&b| b == b'\n')
        .skip(2)
        .map(|line| (
            convert_to_fixed_array(&line[0..3]), 
            vec![
                convert_to_fixed_array(&line[7..10]), 
                convert_to_fixed_array(&line[12..15]), 
            ]
        ))
        .fold(HashMap::new(), |mut node_map, (key, pair)| {
            node_map.insert(key, pair);
            node_map
        })
}

fn convert_to_fixed_array(bytes: &[u8]) -> [u8; 3] {
    let mut temp: [u8; 3] = [0; 3];
    temp.copy_from_slice(&bytes[0..3]);
    temp
}

fn lcm(a: usize, b: usize) -> usize {
    if a == 0 || b == 0 {
        0
    } else {
        a * b / gcd(a, b)
    }
}

fn gcd(mut a: usize, mut b: usize) -> usize {
    while b != 0 {
        let temp = b;
        b = a % b;
        a = temp;
    }
    a
}
