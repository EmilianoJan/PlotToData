#!/bin/bash

executable=monodevelop
app=/mnt/PlotToData.sln

if [ $# -lt 1 ] || [ $# -gt 2 ]; then
  echo "Usage: run.sh [DISPLAY value] <USER>"
  exit 1
fi

if [ $# -eq 2 ]; then
  grep "^$2:" /etc/passwd > /dev/null
  if [ $? -ne 0 ]; then
    useradd "$2"
  fi
  if [ ! -d "/home/$2" ]; then
    mkdir -p "/home/$2"
  fi
  chown -R "$2:$2" "/home/$2" "/home/$2/"
  DISPLAY="$1" su "$2" -c "$executable $app"
else
  DISPLAY="$1" $executable $app
fi
