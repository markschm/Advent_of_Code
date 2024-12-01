use std::collections::HashMap;

fn main() {
    let input = include_bytes!("input/day08");
    let pattern = build_pattern(input); 
    let node_map = build_node_map(input);

    let mut curr_key = b"AAA";
    let mut i = 0;

    println!(
        "Solution 8a: {}",
        loop {
            curr_key = &node_map.get(curr_key).unwrap()[pattern[i % pattern.len()]];
            i += 1;

            if curr_key == b"ZZZ" {
                break i;
            }
        }
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
