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





####Build 合約的指令

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

