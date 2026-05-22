# The Core Vulnerabilities (The Client-Authority Model)

## Memory Manipulation of Runtime Variables (Gold/Health/Currency)

* The Vulnerability: In a single-player architecture, data fields like int currentGold or int playerHealth sit in raw, unmanaged client memory addresses.

* The Exploit Vector: A user running a basic tool like Cheat Engine can scan for a specific integer value (e.g., your starting gold count of 500), buy a tower, search for the new value, and instantly isolate the memory pointer. They can then rewrite that address value to 999999 or freeze it entirely.

* The Live-Service Fix: Shift to a Server-Authoritative State. The client should never execute a transaction locally. Instead, the client sends an input intent network packet: RequestSpawnTower(TowerType type, Vector3 position). The server calculates the cost against its secure server-side copy of the player's balance, deducts the amount, instantiates the asset, and replicates the new layout down to the client.

## Time-Scale Spoofing & Speed Hacks

* The Vulnerability: Unity engine components rely heavily on Time.deltaTime and Time.timeScale to calculate movement vectors, frame updates, and tower cooldown vectors.

* The Exploit Vector: Cheaters use speed-hacking hooks (native code injections or browser thread acceleration tools for WebGL) to force the local runtime engine process to run at 2x or 5x speed. This allows towers to fire faster, waves to end immediately, or units to bypass standard positioning logic.

* The Live-Service Fix: Implement Deterministic Simulation Ticks or Server-Side Delta Validation. The host server maintains the clock. If the server expects a tower to have a fire rate of 1 shot per second, and the client claims it fired 5 shots in a single 1000ms window, the server flags the packet mismatch and rejects the extra hit registration.

## Cleartext WebGL Binary Reverse Engineering

* The Vulnerability: WebGL projects compile C# down into WebAssembly (.wasm) or JavaScript code. Unlike compiled C++ binaries, these web scripts are incredibly easy to read.

* The Exploit Vector: An attacker can download your .wasm or JS compilation payload straight from their browser cache, run it through an extraction tool (like Il2CppDumper or standard JS prettifiers), and view your entire asset pipeline structure, custom method names, and local validation rules. If you hardcoded any administrative keys or secret API endpoints directly into the code, they are exposed immediately.

* The Live-Service Fix: Use aggressive IL2CPP Obfuscation tools during your deployment build phase to scrape out human-readable variable/method definitions. Never store secure endpoints, decryption keys, or authentication strings within client binaries—gate them behind a secure gateway API layer.
