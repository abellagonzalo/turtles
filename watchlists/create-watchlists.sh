#!/bin/bash

cd /Users/gon/git/Turtles/turtles-buy/

echo "Creating Eurekers watchlist..."
dotnet run -- ../watchlists/eurekers-watchlist.txt > ~/Desktop/eurekers.csv
echo "Eurekers watchlist created"

echo "Creating modern graham watchlist..."
dotnet run -- ../watchlists/moderngraham-watchlist.txt > ~/Desktop/moderngraham.csv
echo "Modern Graham watchlist created"

echo "Creating Ibex watchlist..."
dotnet run -- ../watchlists/ibex-watchlist.txt > ~/Desktop/ibex.csv
echo "Ibex watchlist created"