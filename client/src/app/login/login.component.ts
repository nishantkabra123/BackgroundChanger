import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { UserServiceService } from '../user-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  // readonly rootURL = 'https://localhost:44374/';
  email: any;
  password:any;
  error:String="";
  data:any={};
  
  

  constructor(private http: HttpClient,private service:UserServiceService,private router: Router) {
    this.data.choice=""
   }

  ngOnInit(): void {
      /* this.rootURL +  */
      this.http.get('Home/login').subscribe(res => {
        console.log(res)
        this.data=res
        // console.log(this.data,this.data.choice)
      },
        err => {
          console.log(err);
        })  

  }

  login() {
    let formData =new FormData()
    formData.append("email",this.email)
    formData.append("password",this.password)
    // console.log(this.email,this.password)    
    this.http.post('Home/Login',formData).subscribe(res => {
      // console.log(res)
      if(res!==null && res!==undefined && res!==""){
      this.service.user=res
      this.router.navigate(['image-upload'])
      }else{
        this.error="Invalid username or password"
      }

    },
      err => {
        console.log(err);
      })  
  }

}
