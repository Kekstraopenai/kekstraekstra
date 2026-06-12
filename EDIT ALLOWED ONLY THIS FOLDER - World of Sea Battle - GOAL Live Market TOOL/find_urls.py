import re

dll_paths = [
    r"c:\Games\World of Sea Battle\default\game\WorldOfSeaBattleClient.dll",
    r"c:\Games\World of Sea Battle\default\game\Libs\Common.dll"
]

url_pattern = re.compile(rb'https?://[a-zA-Z0-9./_-]+')

for path in dll_paths:
    print(f"Searching: {path}")
    try:
        with open(path, "rb") as f:
            content = f.read()
            matches = url_pattern.findall(content)
            unique_matches = sorted(list(set(matches)))
            for match in unique_matches:
                print(f"  {match.decode('utf-8', errors='ignore')}")
    except Exception as e:
        print(f"  Error reading file: {e}")
