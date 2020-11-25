import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  imageUrl: string = "/assets/img/default-image.png";
  fileToUpload: any = null;
  // readonly rootURL = 'https://localhost:44374/';

  constructor( private http : HttpClient,private router: Router ) { }

  ngOnInit(): void {
    // const formData = new FormData();
    // formData.append("ImageFile","hi");
    // formData.append('Title', 'caption');
    // console.log(formData)
  }

  handleFileInput(e: any) {
    console.log(e)
    const file:any =e.target.files
    this.fileToUpload = file.item(0);
    console.log(file,this.fileToUpload)
    //Show image preview.
    var reader = new FileReader();
    reader.onload = (event:any) => {
      this.imageUrl = event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload);
    console.log(this.fileToUpload,this.imageUrl)
  }

  postFile(caption: string, fileToUpload: File) {
    const formData: FormData = new FormData();
    formData.append('ImageFile', fileToUpload);
    formData.append('Title', caption);
    return this.http.post("image/create", formData);
  }

  OnSubmit(Caption: any,Image: any){
    this.postFile(Caption.value,this.fileToUpload).subscribe(
      data =>{
        console.log('done');
        Caption.value =null;
        Image.value = null;
        this.imageUrl = "/assets/img/default-image.png";
        this.router.navigate(['image-upload'])
      }
    );          
   }

}
