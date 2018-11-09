# NNS Smart Contract on NEO with Python



We have already tested our NNS contract on private chain for a while, now we are about to deploy our contract on testnet!



## NNS ABI in Portal Network



### owner (domain_name) : 

Query domain, input domian name and return the owner of the domain.



### release(domain_name):

Release the domain which already owned by owner. Make this domain avaliable again.



### register (domain_name, owner):

Register a domain that can used all kind of the service.



### transfer (domain_name, new owner):

Transfer the ownership from owner to another address.



### setAddress (domain_name, address):

Let the domain owner can set the address owner want to map from domain.



### addIPFS (domain_name, IPFS hash):

Let domain owner can bind IPFS hash with the domain name.



### setSubdomain (domain_name, subdomain):

Let domain owner can set a subdomain with their domain.



### getAddress (domain_name):

Query the address owner registered with this domain.



### getIPFS (domain_name):

Query the IPFS owner registered with this domain.



# NNS Smart Contract on NEO with Python

#### NNS funciton 解釋

在`Main(operation, args)` 中，透過不同的`operation` 來進行操作。



 `QueryDomain(domain_name)`：domain查詢功能，透過`domain_name` 查詢對應的domain，如果域名已註冊，retrun `owner address`，否則return `False`。



 `RegisterDomain(domain_name, owner)`：註冊domain，我們只能使用自己錢包位置來註冊，使用`CheckWitness(owner)` 進行驗證，驗證該address 是否為錢包所擁有，然後判斷domain是否已被註冊。



 `TransferDomain(domain_name, to_address)`：轉讓domain，把`domain_name`    把所有權轉讓到    `to_address`。首先會驗證要轉讓的domain是否存在，然後驗證這個domain是否為msg.sender所有，最後驗證轉讓的address是否正確。



 `DeleteDomain(domain_name)`：首先驗證domain是否存在，然後驗證這個domain是否為msg.sender所有，删除domain。



`SetAddress(domain_name,to_address)` : 首先驗證domain是否存在，然後驗證這個domain是否為msg.sender所有,最後檢查waller address格式是否正確，wallet address 綁定 domain。



`GetAddress(domain_name)` : 查詢domain綁定的wallet address。



`AddIPFSHash(domain_name,IPFS):`  首先驗證domain是否存在，然後驗證這個domain是否為msg.sender所有，把ipfs hash綁定domain。



 `SetSubdomain(domain_name,subdomain,to_address):`首先驗證domain是否存在，然後驗證這個domain是否為msg.sender所有，最後檢查waller address格式是否正確，把註冊的subdomain轉給設定的address。





#### Build 合約的指令

```
build {path/to/file.py} (test {params} {returntype} {needs_storage} {needs_dynamic_invoke} {test_params})
```



#### **params**和**returntype**：

| Params           | Returntype |
| ---------------- | ---------- |
| Signature        | 00         |
| Boolean          | 01         |
| Integer          | 02         |
| Hash160          | 03         |
| Hash256          | 04         |
| ByteArray        | 05         |
| PublicKey        | 06         |
| String           | 07         |
| Array            | 10         |
| InteropInterface | f0         |
| void             | ff         |







Now we are already deployed the NNS contract on NEO testate, anyone who want to register a domain 









## The testinvoke of our NNS contract

```
neo> build domain.py test 0710 05 True False False owner ["johnny"]                                                                                           
[I 181029 17:04:00 BuildNRun:52] Saved output to domain.avm 
[I 181029 17:04:00 Invoke:577] Used 0.178 Gas 

-----------------------------------------------------------
Calling domain.py with arguments ['["johnny"]', 'owner'] 
Test deploy invoke successful
Used total of 113 operations 
Result [{'type': 'ByteArray', 'value': ''}] 
Invoke TX gas cost: 0.0001 
-------------------------------------------------------------

neo> build domain.py test 0710 05 True False False register ["johnny","AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y"]                                                   
[I 181029 17:04:10 BuildNRun:52] Saved output to domain.avm 
[I 181029 17:04:10 Invoke:577] Used 1.411 Gas 

-----------------------------------------------------------
Calling domain.py with arguments ['["johnny","AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y"]', 'register'] 
Test deploy invoke successful
Used total of 172 operations 
Result [{'type': 'ByteArray', 'value': '01'}] 
Invoke TX gas cost: 0.0001 
-------------------------------------------------------------

neo> build domain.py test 0710 05 True False False owner ["johnny"]                                                                                           
[I 181029 17:04:13 BuildNRun:52] Saved output to domain.avm 
[I 181029 17:04:13 Invoke:577] Used 0.182 Gas 

-----------------------------------------------------------
Calling domain.py with arguments ['["johnny"]', 'owner'] 
Test deploy invoke successful
Used total of 118 operations 
Result [{'type': 'ByteArray', 'value': '23ba2703c53263e8d6e522dc32203339dcd8eee9'}] 
Invoke TX gas cost: 0.0001 
-------------------------------------------------------------

neo> build domain.py test 0710 05 True False False setAddress ["johnny","AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y"]                                                 
[I 181029 17:04:19 BuildNRun:52] Saved output to domain.avm 
[I 181029 17:04:19 Invoke:577] Used 1.419 Gas 

-----------------------------------------------------------
Calling domain.py with arguments ['["johnny","AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y"]', 'setAddress'] 
Test deploy invoke successful
Used total of 187 operations 
Result [{'type': 'ByteArray', 'value': '01'}] 
Invoke TX gas cost: 0.0001 
-------------------------------------------------------------

neo> build domain.py test 0710 05 True False False getAddress ["johnny"]                                                                                      
[I 181029 17:04:26 BuildNRun:52] Saved output to domain.avm 
[I 181029 17:04:26 Invoke:577] Used 0.344 Gas 

-----------------------------------------------------------
Calling domain.py with arguments ['["johnny"]', 'getAddress'] 
Test deploy invoke successful
Used total of 223 operations 
Result [{'type': 'ByteArray', 'value': '23ba2703c53263e8d6e522dc32203339dcd8eee9'}] 
Invoke TX gas cost: 0.0001 
-------------------------------------------------------------

```

