import 'package:flutter/material.dart';
import 'package:hackathon/getEventDetailsByCode.dart';
import 'package:hackathon/main.dart';

class TicketDetails extends StatefulWidget {
 static const  String routeName = '/details';

  const TicketDetails({ Key? key }) : super(key: key);

  @override
  State<TicketDetails> createState() => _TicketDetailsState();
}

class _TicketDetailsState extends State<TicketDetails> {
  Map<String, dynamic> registration = {};
  bool isLoading = true;

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    Future.delayed(Duration.zero, getDetails);
  }

  void getDetails() async {
    try {
      setState(() {
        isLoading = true;
      });
      final args = ModalRoute.of(context)?.settings.arguments as Map<String, String?>;
    if(args['code'] != null) {
    final newReg = await getEventDetailsByCode(args['code']!);
    setState(() {
      registration = newReg;
      isLoading = false;
    });
    }
    } catch (e) {
      print(e);
    }
  }


  @override
  Widget build(BuildContext context) {
  print(registration);
    return Scaffold(
      body: isLoading ? Center(child: CircularProgressIndicator(),) :Padding(
        padding: const EdgeInsets.all(18.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
           children: [
             Container(
               decoration: BoxDecoration(border: Border.all(color: Colors.black, width: 1),borderRadius: BorderRadius.all(Radius.circular(22),),),

               padding: const EdgeInsets.all(18),
               width: double.infinity,
               child: Column(
                 children: [
                  
                  if(registration["banner"] != null) Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Container(child: Image.network('https://97fd-2402-8100-300c-8413-10cf-58bd-99b1-428a.ngrok.io/images/${registration["banner"]}', width: 100, height: 100), decoration: BoxDecoration(borderRadius: BorderRadius.all(Radius.circular(50)),), clipBehavior: Clip.hardEdge,)
                    ],
                  ),
                  SizedBox(height: 14),
                   Row(children: [Text(registration["username"] ?? "", textAlign: TextAlign.right, style: Theme.of(context).textTheme.bodyLarge?.copyWith(fontSize: 18),)],mainAxisAlignment: MainAxisAlignment.center,),
                   SizedBox(height: 24),
                   Row(children: [Text(registration['eventName']?? "", textAlign: TextAlign.right, style: Theme.of(context).textTheme.bodyLarge?.copyWith(fontSize: 24),)],mainAxisAlignment: MainAxisAlignment.center,),
                   SizedBox(height: 16),
                   Row(children: [Text(registration['subEventName'] ?? "", textAlign: TextAlign.right, style: Theme.of(context).textTheme.bodyLarge?.copyWith(fontSize: 20),)],mainAxisAlignment: MainAxisAlignment.center,),

                 ],
               ),
             ),
             SizedBox(height: 30),
            Text(registration['code'] ?? "", style: Theme.of(context).textTheme.bodyLarge?.copyWith(fontSize: 24),),
        
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton.extended(
        onPressed: () {
          Navigator.of(context).pushNamed(HomeScreen.routeName);
          // Add your onPressed code here!
        },
        backgroundColor: Colors.blue,
        label: Text("New Scan"),
        icon: Icon(Icons.camera_alt),
      ),
      floatingActionButtonLocation: FloatingActionButtonLocation.centerFloat,
    );
  }
}