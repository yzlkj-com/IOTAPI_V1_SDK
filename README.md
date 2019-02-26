# IOTAPI_V1_SDK

## 使用流程
	步骤：开发者如需使用API ，请先创建API等信息。
	
## 请求头说明
 * YZL-APIKEY : 你的 apikey
 * YZL-SIGN : 使用base64编码的签名(请参阅下面签名详细说明)。
 * YZL-TIME : 发起请求的时间, 例如 "2019-01-24T12:19:31.552+08:00"。
 * YZL-PASSPHRASE : 你的 passphrase。

## 签名
	签名是通过timestamp + method + requestPath + body字符串(+表示字符串连接)，以及secret，使用HMACSHA256方法加密，通过BASE64编码输出而得到的。
	例如： sign=CryptoJS.enc.Base64.stringify(CryptoJS.HmacSHA256(timestamp + 'GET' + '/device/list', secret))
	
## [使用示例](https://github.com/yzlkj-com/IOTAPI_V1_SDK)


## /api/v1/device/
<a id=/api/v1/device/> </a>
### 基本信息

**Path：** /api/v1/device/

**Method：** GET

**接口描述：**
#### - 说明
	获取账户下指定设备信息。

#### - 示例
	http://api.yzlkj.com/api/v1/device/TEST-0001

#### - 参数
| key | type | Must | Default | instructions |
|:---:|:----:|:----:|:-------:|:------------:| 
| deviceIds | string | true | 1 | 设备id, 多个设备用","间隔。| 

#### - 返回
```json
{
	"statusCode": 0, // 状态码, 0成功
	"message": "OK", // 状态信息
	"data": 
	[
		{
			"model": "", // 设备型号
			"id": "", // 设备id
			"isOnline": false, // 是否在线
			"envs": // 设备传感器
			[ 
				{
					"instructions": "", // 传感器描述
					"lastTime": "", // 最后一笔上传时间
					"serialNumber": "", // 传感器序号
					"value": "", // 数值
					"type": "", // 传感器类型
					"state": 0 // 传感器好坏, 0坏\1好
				}				
				...
			],
			"controls": // 设备开关量
			[
				{
					"instructions": "", // 传感器描述
					"lastTime": "", // 最后一笔上传时间
					"serialNumber": "", // 传感器序号
					"switc": 0, // 开关, 0关\1开
					""way"" : 0, // 触发方式, 0手动\1自动\2下发
					"type": "", // 传感器类型
					"state": 0 // 传感器好坏, 0坏\1好
				}
				...
			]
		}
		...
	]
}

```

## /api/v1/device/list
<a id=/api/v1/device/list> </a>
### 基本信息

**Path：** /api/v1/device/list

**Method：** GET

**接口描述：**
#### - 说明
	获取账户下设备信息列表。

#### - 示例
	http://api.yzlkj.com/api/v1/device/list?page=1&pageSize=5

#### - 参数
| key | type | Must | Default | instructions |
|:---:|:----:|:----:|:-------:|:------------:| 
| page | number | false | 1 | 页码 | 
| pageSize | number | false | 50 | 页大小，最大50 | 
	
#### - 返回
```json
{
	"statusCode": 0, // 状态码, 0成功
	"message": "OK", // 状态信息
	"data": 
	{
		deviceListTotalNumber: 0, // 设备总数
		page: 0, // 页码
		pageSize: 0, // 页大小
		actualNumber: 0, // 实际数量
		deviceInfos:
		[
			{
				"model": "", // 设备型号
				"id": "", // 设备id
				"isOnline": false, // 是否在线
				"envs": // 设备传感器
				[ 
					{
						"instructions": "", // 传感器描述
						"lastTime": "", // 最后一笔上传时间
						"serialNumber": "", // 传感器序号
						"value": "", // 数值
						"type": "", // 传感器类型
						"state": 0 // 传感器好坏, 0坏\1好
					}				
					...
				],
				"controls": // 设备开关量
				[
					{
						"instructions": "", // 传感器描述
						"lastTime": "", // 最后一笔上传时间
						"serialNumber": "", // 传感器序号
						"switc": 0, // 开关, 0关\1开
						""way"" : 0, // 触发方式, 0手动\1自动\2下发
						"type": "", // 传感器类型
						"state": 0 // 传感器好坏, 0坏\1好
					}
					...
				]
			}
			...
		]
	}
}

```

