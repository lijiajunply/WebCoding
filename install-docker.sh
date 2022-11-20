#!/bin/bash
sudo apt update
sudo apt upgrade
sudo pkcon update #kde neno
sudo apt full-upgrade
sudo apt install apt-transport-https ca-certificates curl software-properties-common gnupg lsb-release
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt update
sudo apt install docker-ce 
sudo apt install docker-ce-cli 
sudo apt install containerd.io 
sudo apt install docker-compose-plugin
sudo docker ps
sudo gpasswd -a $USER docker 
newgrp docker
sudo systemctl restart docker
sudo -i
docker pull ubuntu
