#!/bin/bash

read -p "Input migration name: " name
echo "Will create migration: $name"

dotnet ef migrations add $name --project ../Todos.csproj --output-dir ./Database/Migrations
