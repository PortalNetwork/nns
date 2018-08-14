# Neo Smart Contract From Development To Private Net

1. [download and install docker](https://download.docker.com/mac/stable/Docker.dmg)

1. execute command ```docker run -d --name neo-privnet-with-gas -p 20333-20336:20333-20336/tcp -p 30333-30336:30333-30336/tcp -v "$(pwd)":/opt/neo-python/smartContracts metachris/neo-privnet-with-gas```

1. run neo's privnet ```docker exec -it neo-privnet-with-gas /bin/bash```  
![](https://i.imgur.com/mHDlagb.png)

1. run neo private net ```python3 /opt/neo-python/prompt.py -c protocol.privnet.json``` 
![](https://i.imgur.com/opDfFfU.png)

1. create wallet named portalnetwork.wallet ```neo> create wallet portalnetwork.wallet```
![](https://i.imgur.com/Kaherpk.png)

1. at this moment, portalnetwork.wallet has no NEO and NEOGas. run this command to import NEO and NEOGas ```neo> import wif KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr``` 

1. ```neo>wallet rebuild```
1. ```neo>wallet```
![](https://i.imgur.com/HEgOsWF.png)

1. exit neo privnet and under your neo smart contract project folder. Copy your smart contract files to neo's privnet container ```$  docker cp domain.py [container id]:/opt/neo-python/contract/domain.py```. 

1. back into docker, use neo-boa build python to avm. run python3 and execute two command```from boa.compiler import Compiler``` then ```Compiler.load_and_save('one.py')``` ![](https://i.imgur.com/q5pPrWN.png)

1. execute neo's privnet ```python3 /opt/neo-python/prompt.py -c protocol.privnet.json```

1. before deploy contract, you should open wallet. ```open wallet portalnetwork.wallet```

1. deploy contract ```neo> import contract ./contract/domain.avm "" 01 False False```
![](https://i.imgur.com/a0XQOJO.png)

1. ```neo> contract search [your contract name]```
![](https://i.imgur.com/YXMrjzD.png)

1. test contract via command ```neo> testinvoke 68ebfc4fefbe24c9cff0f7e3c0d27ed396d07f9f```
![](https://i.imgur.com/psWIeVe.png)


