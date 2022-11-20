sudo -i
docker run -ti ubuntu bash
apt update
apt upgrade
apt install gcc g++ openjdk-17-jdk python2 python3
apt-get install -y dotnet6
apt install wget
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
