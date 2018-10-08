#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Mon Oct  8 14:33:19 2018

@author: johnnyhsieh
"""

#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Sat Oct  6 17:04:49 2018

@author: johnnyhsieh
"""


from boa.interop.Neo.Runtime import Log, Notify
from boa.interop.Neo.Storage import Get, Put, GetContext
from boa.interop.Neo.Runtime import GetTrigger,CheckWitness
from boa.builtins import concat


def Main(operation, args):
    nargs = len(args)
    if nargs == 0:
        print("No domain name supplied")
        return 0

    if operation == 'owner':
        domain_name = args[0]
        return QueryDomain(domain_name)

    elif operation == 'release':
        domain_name = args[0]
        return DeleteDomain(domain_name)

    elif operation == 'register':
        if nargs < 2:
            print("required arguments: [domain_name] [owner]")
            return 0
        domain_name = args[0]
        owner = args[1]
        return RegisterDomain(domain_name, owner)

    elif operation == 'transfer':
        if nargs < 2:
            print("required arguments: [domain_name] [to_address]")
            return 0
        domain_name = args[0]
        to_address = args[1]
        return TransferDomain(domain_name, to_address)
    
    elif operation == 'setAddress':
       if nargs < 2:
           print("required arguments: [domain_name] [to_address]")
           return 0
       domain_name = args[0]
       to_address = args[1]
       return SetAddress(domain_name,to_address)
   
    elif operation == 'addIPFS':
        if nargs < 2:
             print("required arguments: [domain_name] [IPFSHash]")
             return 0
        domain_name = args[0]
        IPFS = args[1]
        return AddIPFSHash(domain_name,IPFS)
    
    elif operation == 'setSubdomain':
        if nargs == 0:
            print("required arguments: [domain_name] [subdomain]")
            return 0
        domain_name = args[0]
        subdomain = args[1]
        return SetSubdomain(domain_name,subdomain)
        




def QueryDomain(domain_name):
    msg = concat("QueryDomain: ", domain_name)
    Notify(msg)

    context = GetContext()
    owner = Get(context, domain_name)
    if not owner:
        Notify("Domain is not yet registered")
        return False

    Notify(owner)
    return owner


def RegisterDomain(domain_name, owner):
    msg = concat("RegisterDomain: ", domain_name)
    Notify(msg)

    if not CheckWitness(owner):
        Notify("Owner argument is not the same as the sender")
        return False

    context = GetContext()
    exists = Get(context, domain_name)
    if exists:
        Notify("Domain is already registered")
        return False

    Put(context, domain_name, owner)
    return True


def TransferDomain(domain_name, to_address):
    msg = concat("TransferDomain: ", domain_name)
    Notify(msg)

    context = GetContext()
    owner = Get(context, domain_name)
    if not owner:
        Notify("Domain is not yet registered")
        return False

    if not CheckWitness(owner):
        Notify("Sender is not the owner, cannot transfer")
        return False

    if not len(to_address) != 34:
        Notify("Invalid new owner address. Must be exactly 34 characters")
        return False

    Put(context, domain_name, to_address)
    return True


def DeleteDomain(domain_name):
    msg = concat("DeleteDomain: ", domain_name)
    Notify(msg)

    context = GetContext()
    owner = Get(context, domain_name)
    if not owner:
        Notify("Domain is not yet registered")
        return False

    if not CheckWitness(owner):
        Notify("Sender is not the owner, cannot transfer")
        return False

    Delete(context, domain_name)
    return True

def SetAddress(domain_name,to_address):
    msg = concat("SetAddress: ", domain_name)
    Notify(msg)
    context = GetContext()
    owner = Get(context, domain_name)
    if not CheckWitness(owner):
        Notify("Owner argument is not the same as the sender")
        return False
    if not len(to_address) != 34:
        Notify("Invalid new owner address. Must be exactly 34 characters")
        return False
    
    Put(context,"{domain_name}.neo", to_address)
    return True
    


def GetAddress(domain_name):
    msg = concat("GetAddress: ", domain_name)
    Notify(msg)
    if QueryDomain(domain_name) == False:
       return False
    context = GetContext()
    address = Get(context, "{domain_name}.neo")
    return address

def AddIPFSHash(domain_name,IPFS):
    msg = concat("AddIPFS: ",domain_name)
    Notify(msg)
    context = GetContext()
    owner = Get(context, domain_name)
    if not owner:
        Notify("Domain is not yet registered")
        return False
    
    if not CheckWitness(owner):
        Notify("Sender is not the owner, cannot transfer")
        return False
    
    Put(context,"{domain_name}.ipfs",IPFS)
    return True

    
def SetSubdomain(domain_name,subdomain):
    msg = concat("SetSubdomain: ",domain_name)
    Notify(msg)
    context = GetContext()
    owner = Get(context, domain_name)
    if not owner:
        Notify("Domain is not yet registered")
        return False
    
    if not CheckWitness(owner):
        Notify("Sender is not the owner, cannot transfer")
        return False

    Put(context, "{subdomain}.{domain_name}", owner)
    return True






