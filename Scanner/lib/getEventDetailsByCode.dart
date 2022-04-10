import 'package:dio/dio.dart';

Future<Map<String, dynamic>> getEventDetailsByCode(String code) async {
  try {
    var response = await Dio().post('https://97fd-2402-8100-300c-8413-10cf-58bd-99b1-428a.ngrok.io/registration/verifyRegistrations', data: {'qrCodeId': code});

  
  return response.data['body'];
  } catch(e){
    print(e);
    return {};
  }
}