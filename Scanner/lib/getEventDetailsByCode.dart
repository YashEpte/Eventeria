import 'package:dio/dio.dart';

Future<Map<String, dynamic>> getEventDetailsByCode(String code) async {
  try {
    var response = await Dio().post('https://83a9-2402-8100-300e-dc36-3a0d-f3a9-1c92-3ab2.ngrok.io/registration/verifyRegistrations', data: {'qrCodeId': code});

  
  return response.data['body'];
  } catch(e){
    print(e);
    return {};
  }
}