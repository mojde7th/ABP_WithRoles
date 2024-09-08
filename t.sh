#!/bin/bash

# Reset environment variables
unset version
unset base_name
unset timestamp
unset output_folder
unset changed_files
unset all_changed_files
unset zip_file
unset output_path

# Prompt for version input
echo "Enter version (e.g., 1.0.0): "
read version

# Validate the version input
if [[ ! $version =~ ^[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
    echo "Invalid version format. Please use a format like 1.0.0."
    exit 1
fi

# Set up parameters with version and date-time (without seconds)
base_name="changed_files"
timestamp=$(date +"%Y%m%d_%H%M")
output_folder="${base_name}_v${version}_${timestamp}"
changed_files="changed_files_v${version}_${timestamp}.txt"
all_changed_files="all_changed_files_v${version}_${timestamp}.txt"
zip_file="${output_folder}.zip"

# Define the output directory, two paths before the root directory
output_path="../../${output_folder}"

# Create output directory
mkdir -p "$output_path"

# Get the list of changed files, excluding certain patterns (keywords, folders, and .sh files)
git status --porcelain | awk '{print $2}' | grep -Ev 'diff_|changed_file|/Migrations/|/test/|(^|/)test(/|$)|(^|/)\.github(/|$)|(^|/)\.git(/|$)|(^|/)git(/|$)|\.sh$' > "$output_path/$changed_files"

# Create a text file with the content of all changed files, excluding files with certain keywords and in specific folders
> "$output_path/$all_changed_files"
while IFS= read -r file; do
    if [[ ! "$file" =~ diff_|changed_file|/Migrations/|/test/|(^|/)test(/|$)|(^|/)\.github(/|$)|(^|/)\.git(/|$)|(^|/)git(/|$)|\.sh$ ]]; then
        echo "=== $file ===" >> "$output_path/$all_changed_files"
        cat "$file" >> "$output_path/$all_changed_files"
        echo -e "\n\n" >> "$output_path/$all_changed_files"
    fi
done < "$output_path/$changed_files"

# Copy the changed files to the output folder, excluding files with certain keywords and in specific folders
while IFS= read -r file; do
    if [[ ! "$file" =~ diff_|changed_file|/Migrations/|/test/|(^|/)test(/|$)|(^|/)\.github(/|$)|(^|/)\.git(/|$)|(^|/)git(/|$)|\.sh$ ]]; then
        mkdir -p "$(dirname "$output_path/$file")"
        cp "$file" "$output_path/$file"
    fi
done < "$output_path/$changed_files"

# Zip the entire output folder, including the text file and changed files
cd "$output_path/.."
zip -r "$zip_file" "$output_folder"

# Return to the original directory
cd - > /dev/null

echo "Filtered list of changed files has been saved in: $output_path/$changed_files"
echo "Content of filtered changed files saved in: $output_path/$all_changed_files"
echo "Zip file of filtered changed files created: $zip_file"
