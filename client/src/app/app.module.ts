import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { UserServiceService } from './user-service.service';
import { ImageUploadComponent } from './image-upload/image-upload.component';

import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CreateComponent } from './create/create.component'; // CLI imports router

const routes: Routes = [
  { path: '', component: LoginComponent,pathMatch: 'full' },
  { path: 'image-upload', component: ImageUploadComponent },
  { path: 'create', component: CreateComponent },

  // { path: '',   redirectTo: '/',  }, // redirect to `first-component`ssss
  { path: '**', redirectTo: '' },
];

@NgModule({
  declarations: [
    AppComponent,
    ImageUploadComponent,
    LoginComponent,
    CreateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    HttpClientModule,
    [RouterModule.forRoot(routes)]
  ],
  exports: [RouterModule],
  providers: [UserServiceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
