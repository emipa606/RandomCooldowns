# Copilot Instructions for "Random Cooldowns (Continued)" Mod

## Mod Overview and Purpose

"Random Cooldowns (Continued)" is an update of the original mod by Iron Xidess. The primary aim of this mod is to introduce variability in the cooldown times for ranged attacks based on the shooting skill of pawns. This randomness in cooldowns creates a more dynamic and chaotic combat experience, which can make the game's combat more challenging and less predictable.

### Purpose
- Introduces a weighted randomness in the cooldown of ranged weapon attacks.
- Provides a game mechanic that causes combat actions to feel less mechanical and more realistic.

## Key Features and Systems

- **Random Cooldown Modification:** Adds a random component to the cooldown times of ranged weapons. 
- **Skill-Based Adjustment:** The randomness is weighted by the pawn's shooting skill. Lower skill generally leads to increased cooldowns, while higher skill tends to reduce them.
- **Balance in Variability:** Cooldown decreases (bonuses) are more frequent but smaller compared to increases, maintaining an overall mean cooldown time.
- **Weapon Compatibility:** All vanilla ranged weapons and most modded weapons are affected, except modded weapons with very short cooldowns.
- **Compatibility and Integration:** Does not require any particular load order and should be safe to add or remove from existing save games. Compatible with almost all mods, barring those that heavily modify ranged attack code.

## Coding Patterns and Conventions

- **C# Code Structure:** 
  - Use well-structured and commented code for clarity.
  - Follow the naming conventions where class names are in PascalCase and method names are in camelCase.
  - Ensure logical separation of concerns across classes.

- **Handling Randomness:** 
  - Use C#'s `Random` class carefully to ensure consistent random behavior across game sessions.

## XML Integration

- **About.xml and Manifest.xml Management:**
  - Ensure that these XML files correctly define the mod metadata including dependencies such as `brrainz.harmony`.

## Harmony Patching

- **Patch Structure:**
  - Utilize Harmony for non-intrusive code alterations. 
  - `HarmonyPatches.cs` should be used to define patches using attributes such as `[HarmonyPatch]`.
  - Implement postfix operations where necessary, such as in `Verb_TryCastNextBurstShot.cs`, to alter behavior after the original method is executed.

## Suggestions for Copilot

1. **Pattern Recognition:** Copilot can be optimized to recognize and suggest patterns in Harmony patch usage, particularly for postfixes and prefixes.
2. **Template Setup:** Provide efficient templates for setting up new Harmony patches and XML configuration files.
3. **Debugging Assistance:** Offer suggestions for logging and error-handling instrumentation to aid in troubleshooting issues.
4. **Mod Compatibility Checks:** Assist in automating checks for conflicts with other mods, particularly those altering ranged weapon mechanics.

This guidance provides a foundational framework and strategic approach for using GitHub Copilot effectively in the development of the "Random Cooldowns (Continued)" mod. By adhering to these practices, contributors can maintain consistency, reliability, and compatibility across updates and integrations.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

