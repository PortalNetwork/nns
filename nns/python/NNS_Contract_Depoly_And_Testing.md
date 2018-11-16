# NNS Contract Deployed on NEO Testnet:

We have been developed NEO smart contract for a while, and finally deployed our NNS contract on NEO testnet.

This contract has been tested for several times, and now opening for those who want to try the NNS system, NNS on the testnet are fully free to register domains only charge in system fee ( NEO Gas ) which NEO system now don't charge fee to those activity under 50 Gas. Now we have achieved most of the ENS (Ethereum Naming Service) function on NNS. And these function are Register, Owner, Transfer, SetAddress, SetSubdomain, GetAddress, AddIPFS,  GetIPFS, Release.

**Notice:**  This is the beta contract on testnet, the offical contract on the mainnet might be different from this contract. 





## Contract Deploy:

```
neo> import contract NEO_testNet_offical.avm 0710 07 True False False                                                                                         
Please fill out the following contract details:
[Contract Name] > NNS_Portal                                                                                                                                  
[Contract Version] > 0.1.0                                                                                                                                    
[Contract Author] > JohnnyHsieh                                                                                                                               
[Contract Email] > johnny@portal.network                                                                                                                      
[Contract Description] > This contract has been tested for several times, and now opening for those who want to try the NNS system, NNS on the testnet are ful
ly free to register domains only charge in system fee ( NEO Gas ) which NEO system now don't charge fee to those activity under 50 Gas. Now we have achieved m
ost of the ENS (Ethereum Naming Service) function on NNS. And these function are Register, Owner, Transfer, SetAddress, SetSubdomain, GetAddress, AddIPFS,  Ge
tIPFS, Release. 
 
**Notice:**  This is the beta contract on testnet, the offical contract on the mainnet might be different from this contract.  
                                                                                                                                                              
Creating smart contract....
                 Name: NNS_Portal 
              Version: 0.1.0
               Author: JohnnyHsieh 
                Email: johnny@portal.network 
          Description: This contract has been tested for several times, and now opening for those who want to try the NNS system, NNS on the testnet are fully free to register domains only charge in system fee ( NEO Gas ) which NEO system now don't charge fee to those activity under 50 Gas. Now we have achieved most of the ENS (Ethereum Naming Service) function on NNS. And these function are Register, Owner, Transfer, SetAddress, SetSubdomain, GetAddress, AddIPFS,  GetIPFS, Release.

**Notice:**  This is the beta contract on testnet, the offical contract on the mainnet might be different from this contract. 
 
        Needs Storage: True 
 Needs Dynamic Invoke: False 
           Is Payable: False 
{
    "hash": "0xc7550e4622313628e61e7b8db13cfdcc4cb55eb9",
    "script": "0143c56b6a00527ac46a51527ac46a51c3c06a5....966796168164e656f2e53746f726167652e476574436f6e74657874616a52527ac46a52c36a00c37c680f4e656f2e53746f726167652e476574616a53527ac46a53c36339001c446f6d61696e206973206e6f7420796574207265676973746572656468124e656f2e52756e74696d652e4e6f7469667961006c7566616a53c368124e656f2e52756e74696d652e4e6f74696679616a53c36c75665ec56b6a00527ac46a51527ac46a51c36a00c3946a52527ac46a52c3c56a53527ac4006a54527ac46a00c36a55527ac461616a00c36a51c39f6433006a54c36a55c3936a56527ac46a56c36a53c36a54c37bc46a54c351936a54527ac46a55c36a54c3936a00527ac462c8ff6161616a53c36c7566",
    "parameters": "0710",
    "returntype": 7
}
Used 500.0 Gas 

-------------------------------------------------------------------------------------------------------------------------------------
Test deploy invoke successful
Total operations executed: 11 
Results:
[<neo.Core.State.ContractState.ContractState object at 0x1c4ea5358>]
Deploy Invoke TX GAS cost: 490.0 
Deploy Invoke TX Fee: 0.0 
-------------------------------------------------------------------------------------------------------------------------------------


```



NNS Address on Testnet:
0xc7550e4622313628e61e7b8db13cfdcc4cb55eb9





## NNS Function:



### Register function:

Register is how you register domains from NNS contract, you need two parameters to register domain, first the domain you want and second your wallet address.

NNS contract will first check if this domain has been taken by others then transfer this domain to you.

------

Calling NEO_testNet_offical.py with arguments ['["portal","ATRuMWT6tz4sUYxuiMiiMNd7uVDf5z2VFH"]', 'register'] 

Test deploy invoke successful

Used total of 193 operations 

Result [{'type': 'ByteArray', 'value': '01'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



### Owner function:

Owner function is to query the ownership of the domain, you only need one parameter : domain you want to search. NNS contract will return the adress of the domain owner in script hash.

\-----------------------------------------------------------

Calling NEO_testNet_offical.py with arguments ['["portal"]', 'owner'] 

Test deploy invoke successful

Used total of 125 operations 

Result [{'type': 'String', 'value': '7fda9d7b4aa486ba7d4ecdb183842114f1792c6e'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



### Transfer function:

Transfer function is to let domain owner to transfer their domain's ownership, just like the register function, you need two parameters : the domain you want to transfer and the new address you want to transfer your domain with.

First NNS contract will check if you are the domain owner, theres no way you can transfer the domain you don't have the ownership.

\-----------------------------------------------------------

Calling NEO_testNet_offical.py with arguments ['["portal","AZoUxtj8hh5wHYwPoYaFi9xb8TibaBHrNy"]', 'transfer'] 

Test deploy invoke successful

Used total of 208 operations 

Result [{'type': 'String', 'value': '1'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



### SetAddress function:

After you have your own domain, just like ENS, you can also set any wallet address you want with you domain. You need two parameters to process: the domain you owned and the wallet address you want to set with.



------

Calling NEO_testNet_offical.py with arguments ['["johnny","ATRuMWT6tz4sUYxuiMiiMNd7uVDf5z2VFH"]', 'setAddress'] 

Test deploy invoke successful

Used total of 187 operations 

Result [{'type': 'ByteArray', 'value': '01'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



### SetSubdomain function:

As soon as you have at least one domain, you can using it to create subdomain, you need two parameters to create subdomain, first is the domain you owned and second is the subdomain you want.

NNS contract will first check if user has the ownership of domain then transfer subdomain ownership to the user.

------

Calling NEO_testNet_offical.py with arguments ['["johnny","hsieh"]', 'setSubdomain'] 

Test deploy invoke successful

Used total of 248 operations 

Result [{'type': 'String', 'value': '3849b49901'}, {'type': 'String', 'value': '1'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



### GetAddress function:

This funciton is to query the address mapping to NNS domain.

The only parameter you need is the domain you want to query. And the NNS contract will return the script hash.

\-----------------------------------------------------------

Calling NEO_testNet_offical.py with arguments ['["johnny"]', 'getAddress'] 

Test deploy invoke successful

Used total of 230 operations 

Result [{'type': 'String', 'value': '7fda9d7b4aa486ba7d4ecdb183842114f1792c6e'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



### AddIPFS function:

NNS domain can also asign IPFS hash with it.

You need two parameters first is your domain and second is the IPFS hash you want to asign with.

------

Calling NEO_testNet_offical.py with arguments ['["johnny","QmYwAPJzv5CZsnA625s3Xf2nemtYgPpHdWEz79ojWnPbdG"]', 'addIPFS'] 

Test deploy invoke successful

Used total of 191 operations 

Result [{'type': 'String', 'value': '1'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



### GetIPFS function:

This function can let users to get the IPFS hash that domain is mapping to.

Tje only parameter is the domain you want to query.

------

Calling NEO_testNet_offical.py with arguments ['["johnny"]', 'getIPFS'] 

Test deploy invoke successful

Used total of 237 operations 

Result [{'type': 'String', 'value': 'QmYwAPJzv5CZsnA625s3Xf2nemtYgPpHdWEz79ojWnPbdG'}] 

Invoke TX gas cost: 0.0001 

\-------------------------------------------------------------



### Release function:

You can also release the domain you own, that others can register from NNS once again.

The only parameter you need is the domain name.

The NNS contract will check if you are the domain owner, then release it to NNS contract. 

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
