#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Wed Nov 14 09:23:08 2018

@author: johnnyhsieh
"""

from boa.interop.Neo.App import RegisterAppCall

CalculatorContract = RegisterAppCall('86d58778c8d29e03182f38369f0d97782d303cc0', 'operation', 'a', 'b')


def Main(operation, args):
    
    a = args[0]
    b = args[1]

    result = CalculatorContract(operation, a, b)

    return result
