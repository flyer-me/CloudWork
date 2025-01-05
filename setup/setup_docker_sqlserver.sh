# Linux Docker ��װ SQLServer 2019

#!/bin/bash

# ����������б�Ͱ�װ��Ҫ��������
sudo apt-get update && sudo apt-get install -y apt-transport-https ca-certificates curl software-properties-common

# ���Docker�ٷ�GPG��Կ
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg

# �����ȶ���Docker�ֿ�
echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

# �ٴθ���APT����������װDocker����
sudo apt-get update && sudo apt-get install -y docker-ce docker-ce-cli containerd.io

# ��֤docker�Ƿ�װ��ȷ
sudo docker run hello-world

# ��ȡ���µ�SQL Server����
sudo docker pull mcr.microsoft.com/mssql/server:2019-latest

# ����SQL Server������������Ϊ��������
sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Asdf1234' \
  -p 1433:1433 --name sqlserver \
  --mount source=sqlserverdata,target=/var/opt/mssql \
  -d mcr.microsoft.com/mssql/server:2019-latest

docker update --restart=always sqlserver

# ��������Ϣ
docker ps
echo "Docker and SQL Server 2019 setup completed.