import { Component } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { UserServiceService } from './user-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';
  readonly rootURL = 'https://localhost:44374/';
  formData =new FormData()
  email: any;
  password:any;

  constructor(private http: HttpClient,private service:UserServiceService) {
    // this.login().subscribe(res => {
    //   console.log(res)
    // },
    //   err => {
    //     console.log(err);
    //   })  
   }  

login() {
  this.formData.append("email",this.email)
  this.formData.append("password",this.password)
  
  this.http.post(this.rootURL + 'Home/Login', this.formData).subscribe(res => {
    console.log(res)
    this.service.user=res
  },
    err => {
      console.log(err);
    })  
}

}
