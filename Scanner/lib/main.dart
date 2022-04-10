import 'package:flutter/material.dart';
import 'package:hackathon/Ticket.dart';
import 'package:hackathon/scanner.dart';

void main() => runApp(const MyApp());

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    const appTitle = 'Event Scanner';
    return MaterialApp(
      title: appTitle,
      initialRoute: HomeScreen.routeName,
      routes: {
        HomeScreen.routeName: (_) => HomeScreen(),
        TicketDetails.routeName: (_) => TicketDetails(),
        ScannerScreen.routeName: (_) => ScannerScreen(),
      },
    );
  }
}

class HomeScreen extends StatefulWidget {

  static const String routeName = '/';
  const HomeScreen({ Key? key }) : super(key: key);

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          centerTitle: true,
          title: const Text("Event Scanner"),
          
        ),
        body:  Center(
          child: MyCustomForm(
          )
          ),
          floatingActionButton: FloatingActionButton.extended(
        onPressed: () {
          // Add your onPressed code here!
          Navigator.of(context).pushNamed(ScannerScreen.routeName);
        },
        backgroundColor: Colors.blue,
        label: Text("Scan"),
        icon: Icon(Icons.camera_alt),
      ),
      floatingActionButtonLocation: FloatingActionButtonLocation.centerFloat,
      
    );
  }
}

class MyCustomForm extends StatefulWidget {
  MyCustomForm({Key? key}) : super(key: key);

  @override
  State<MyCustomForm> createState() => _MyCustomFormState();
}

class _MyCustomFormState extends State<MyCustomForm> {
  final TextEditingController _codeController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.center,
      
      
      children: <Widget>[

        Padding(
          padding: EdgeInsets.symmetric(horizontal: 80),
          child: TextField(
            textAlign: TextAlign.center,
            keyboardType: TextInputType.number,
            controller: _codeController,
            decoration: InputDecoration(
            hintText: 'Enter the code',
            border: OutlineInputBorder(
            
            ),
            ),
          ),
        ),
        Padding(
          padding: EdgeInsets.symmetric(horizontal: 80),
          child: SizedBox(width: double.infinity, child: ElevatedButton(onPressed: (){
            Navigator.of(context).pushNamed(TicketDetails.routeName, arguments: {
              "code": _codeController.text
            });
          }, child: Text('Submit'), ))
        ),
      ],
    
    );

  }  
}
  