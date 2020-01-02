@echo off

auto-changelog --output "CHANGELOG.md" ^
--template compact --commit-limit false -backfill-limit false ^
--breaking-pattern "\[BREAKING\]" --ignore-commit-pattern "(version number)" ^
--include-branch "upm" --sort-commits "date" ^
& ^
start CHANGELOG.md