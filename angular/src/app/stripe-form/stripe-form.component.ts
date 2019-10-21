import { Component, OnInit, Renderer } from '@angular/core';

@Component({
  selector: 'app-stripe-form',
  templateUrl: './stripe-form.component.html',
  styleUrls: ['./stripe-form.component.css']
})
export class StripeFormComponent implements OnInit {

  globalListener : any;

  constructor(private renderer:Renderer) { }

  openCheckout(){
    var handler = (<any>window).StripeFormComponent({
      key : 'pk_test_UYSu39pyn95AcTe5TEGgCcsF00wyTDSaR9',
      locale : 'auto',
      token : function(token : any){

      }
    }); 

    handler.open({
      amount : 2000
    })

    
    this.globalListener = this.renderer.listenGlobal('window', 'popstate', () => {
      handler.close();
    });

  }


  ngOnInit() {
  }

}
