#!/bin/bash
while [ 1 ]
do
  clear
  xbuild /v:q src/GildedRose.Console/GildedRose.Console.csproj
  xbuild /v:q src/GildedRose.Tests/GildedRose.Tests.csproj
  nunit-console ./src/GildedRose.Tests/bin/Debug/GildedRose.Tests.dll
  sleep 10
done


