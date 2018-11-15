# NNS Contract Testing:





## Contract Deploy:

```
neo> import contract NEO_testNet_offical.avm 0710 07 True False False                                                                                       

Please fill out the following contract details:

[Contract Name] > NNS                                                                                                                                         

[Contract Version] > 0.0.2                                                                                                                                    

[Contract Author] > JohnnyHsieh                                                                                                                               

[Contract Email] > johnny@portal.network                                                                                                                      

[Contract Description] > NNS plays an connecting and entry layer in Web3.0 services. It connects with NEO wallet, blockchain server, decentralized content resources, and decentralized database. This is the very first version on NEO testnet.                                                                            

Creating smart contract....

​                 Name: NNS 

​              Version: 0.0.2

​               Author: JohnnyHsieh 

​                Email: johnny@portal.network 

​          Description: NNS plays an connecting and entry layer in Web3.0 services. It connects with NEO wallet, blockchain server, decentralized content resources, and decentralized database. This is the very first version on NEO testnet. 

​        Needs Storage: True 

 Needs Dynamic Invoke: False 

​          Is Payable: False
```

## Register function:

------

Calling NEO_testNet_offical.py with arguments ['["portal","ATRuMWT6tz4sUYxuiMiiMNd7uVDf5z2VFH"]', 'register'] 

Test deploy invoke successful

Used total of 193 operations 

Result [{'type': 'ByteArray', 'value': '01'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



## Owner function:

\-----------------------------------------------------------

Calling NEO_testNet_offical.py with arguments ['["portal"]', 'owner'] 

Test deploy invoke successful

Used total of 125 operations 

Result [{'type': 'String', 'value': '7fda9d7b4aa486ba7d4ecdb183842114f1792c6e'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



## Transfer function:

\-----------------------------------------------------------

Calling NEO_testNet_offical.py with arguments ['["portal","AZoUxtj8hh5wHYwPoYaFi9xb8TibaBHrNy"]', 'transfer'] 

Test deploy invoke successful

Used total of 208 operations 

Result [{'type': 'String', 'value': '1'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



## SetAddress function:

------

Calling NEO_testNet_offical.py with arguments ['["johnny","ATRuMWT6tz4sUYxuiMiiMNd7uVDf5z2VFH"]', 'setAddress'] 

Test deploy invoke successful

Used total of 187 operations 

Result [{'type': 'ByteArray', 'value': '01'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



## SetSubdomain function:

------

Calling NEO_testNet_offical.py with arguments ['["johnny","hsieh"]', 'setSubdomain'] 

Test deploy invoke successful

Used total of 248 operations 

Result [{'type': 'String', 'value': '3849b49901'}, {'type': 'String', 'value': '1'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



## GetAddress function:

\-----------------------------------------------------------

Calling NEO_testNet_offical.py with arguments ['["johnny"]', 'getAddress'] 

Test deploy invoke successful

Used total of 230 operations 

Result [{'type': 'String', 'value': '7fda9d7b4aa486ba7d4ecdb183842114f1792c6e'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



## AddIPFS function:

------

Calling NEO_testNet_offical.py with arguments ['["johnny","QmYwAPJzv5CZsnA625s3Xf2nemtYgPpHdWEz79ojWnPbdG"]', 'addIPFS'] 

Test deploy invoke successful

Used total of 191 operations 

Result [{'type': 'String', 'value': '1'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



## GetIPFS function:

------

Calling NEO_testNet_offical.py with arguments ['["johnny"]', 'getIPFS'] 

Test deploy invoke successful

Used total of 237 operations 

Result [{'type': 'String', 'value': 'QmYwAPJzv5CZsnA625s3Xf2nemtYgPpHdWEz79ojWnPbdG'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



## Release function:

\-----------------------------------------------------------

Calling NEO_testNet_offical.py with arguments ['["johnny"]', 'release'] 

Test deploy invoke successful

Used total of 159 operations 

Result [{'type': 'String', 'value': '1'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------

### After release:

\-----------------------------------------------------------

Calling NEO_testNet_offical.py with arguments ['["johnny"]', 'owner'] 

Test deploy invoke successful

Used total of 120 operations 

Result [{'type': 'String', 'value': ''}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------
