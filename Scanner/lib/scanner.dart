import 'package:flutter/material.dart';
import 'package:hackathon/Ticket.dart';
import 'package:mobile_scanner/mobile_scanner.dart';

class ScannerScreen extends StatefulWidget {
  const ScannerScreen({ Key? key }) : super(key: key);
  static const String routeName = "/scannner";

  @override
  State<ScannerScreen> createState() => _ScannerScreenState();
}

class _ScannerScreenState extends State<ScannerScreen>with SingleTickerProviderStateMixin {

  
  @override
  Widget build(BuildContext context) {
    return MobileScanner(
          onDetect: (barcode, args) {
            Navigator.of(context).pushNamed(TicketDetails.routeName, arguments: {
              'code':  barcode.rawValue,
            } );
          });
  }
}