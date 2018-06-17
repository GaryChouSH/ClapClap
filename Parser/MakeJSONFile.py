# -*- coding: UTF-8 -*-

import xlrd
import os
import codecs
import json

OUTPUT_DIR_NAME="SettingData"

def writeLog( OutputStr, Dir_name, File_Name):
	if not os.path.exists(Dir_name):
		os.makedirs(Dir_name)
	with codecs.open( Dir_name+"/"+File_Name, encoding='utf-8', mode='w') as wf:
		wf.write(OutputStr)
		print 'write file : %s'%OutputStr

def makeJSON(IntputFileName,OutputFileName,BuildType):
	data = xlrd.open_workbook(IntputFileName)
	
	#直接抓第0個分頁出來
	table = data.sheets()[0]
	nrows = table.nrows
	ncols = table.ncols
	print "nrows %d, ncols %d, BuildType %d" % (nrows,ncols,BuildType)
	
	#第一欄欄位為Key的打包方式
	if BuildType is 1:
		StartFlag=0
		FirstRowFlag = 1
		OutputDict = {}
		for i in xrange(1,nrows):
			# 將Excel內整數的float轉換為int，其他型態保留原型態
			ThisRowData = [(V if type(V) is not float else (int(V) if V==int(V) else V)) for V in table.row_values(i)]
			if ThisRowData[0]!="" and FirstRowFlag:
				FirstRowFlag = 0
				KeyDict = {}
				for Idx, ColName in enumerate(ThisRowData):
					if Idx == 0:
						continue
					KeyDict[ColName] = Idx-1
				OutputDict["ColMap"]=KeyDict
				continue
			
			elif ThisRowData[0]!="" and FirstRowFlag is 0:
				ValueList = []
				for Idx, Vlaue in enumerate(ThisRowData):
					if Idx == 0:
						continue
					ValueList.append(Vlaue)
				OutputDict[int(ThisRowData[0])]=ValueList
				
		OutputStr = json.dumps(OutputDict,separators=(',', ':'))
		writeLog(OutputStr,OUTPUT_DIR_NAME,OutputFileName)
	#idx及欄位名稱不打包(MusicTab)
	elif BuildType is 2:
		OutputList = []
		for i in xrange(2,nrows):
			ThisRowData = [(V if type(V) is not float else (int(V) if V==int(V) else V)) for V in table.row_values(i)] 
			if ThisRowData[0]!="":
				ValueList = []
				for Idx, Vlaue in enumerate(ThisRowData):
					if Idx == 0:
						continue
					ValueList.append(Vlaue)
				OutputList.append(ValueList)
		OutputStr = json.dumps(OutputList,separators=(',', ':'))
		writeLog(OutputStr,OUTPUT_DIR_NAME,OutputFileName)
		print "OutputListCount %d" % (len(OutputList))

	else:
		OutputList = []
		for i in xrange(1,nrows):
			ThisRowData = [(V if type(V) is not float else (int(V) if V==int(V) else V)) for V in table.row_values(i)] 
			if ThisRowData[0]!="":
				OutputList.append(ThisRowData)
		OutputStr = json.dumps(OutputList,separators=(',', ':'))
		writeLog(OutputStr,OUTPUT_DIR_NAME,OutputFileName)
		

def buildJSON():
	data = xlrd.open_workbook("Build.xlsx")
	table = data.sheets()[0]
	nrows = table.nrows
	
	for i in xrange(1,nrows):
		ThisRowData = table.row_values(i)
		ThisFileName = ThisRowData[0]
		makeJSON(ThisFileName+".xlsx", ThisFileName+".json",int(ThisRowData[1]))



buildJSON()
os.system("pause")
