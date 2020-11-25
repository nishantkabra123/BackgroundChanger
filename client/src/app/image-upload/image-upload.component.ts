import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { UserServiceService } from '../user-service.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent implements OnInit {

  // readonly rootURL = 'https://localhost:44374/';  

  selectedOption:string="";

  constructor(private http: HttpClient,public service:UserServiceService,private router: Router) { }

  ngOnInit(): void {
    this.loadImages()
  }

  Delete(imageId: any){
    let id=new FormData()
    id.append("id",imageId)
    this.http.post('image/delete',id).subscribe(res => {
    this.loadImages()  
      // console.log(this.service.images)
    // this.router.navigate(['image-upload'])
    },
      err => {
        console.log(err);
      }) 
  }

  submitImage(){
    console.log(this.selectedOption)
    let data=new FormData()
    data.append("choice",this.selectedOption)
    console.log("data is",data)
    this.http.post('image/submitimage',data).subscribe(res => {
      console.log(res)
    },
      err => {
        console.log(err);
      }) 

  }

  loadImages() {   
    this.http.get('image').subscribe(res => {  
      console.log(this.service.images)   
      this.service.images=res
      console.log(this.service.images)
    },
      err => {
        console.log(err);
      })  
  }

}

