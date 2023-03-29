import { SocialAuthService } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Note } from 'src/_models/note';
import { User } from 'src/_models/user';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.css']
})
export class BodyComponent {
  title = 'angular-google';

  model:any = {}
  user:any;
  loggedIn:any;

  notes : any = []


  constructor(private authService: SocialAuthService, private http : HttpClient) { }

  ngOnInit() {
    this.authService.authState.subscribe((user) => {
      this.loggedIn = (user != null);
      this.model.email = user.email
      console.log(this.model)
      this.login()
    });
  }

  login(){
      this.http.post<User>('https://localhost:5001/api/users/login/', this.model).subscribe({
        next : response => {
          this.user = response
          console.log(response);
          this.getNote()
        },
        error : error => console.log(error.error)
      })
      
  }

  getNote(){
    this.http.get<Note>('https://localhost:5001/api/note/',this.user.id).subscribe({
      next : response => {
        this.notes = response
        console.log(response)
      },
      error : error => console.log(error.error)
    })
  }

  changeText : any = {}

  change(){
    console.log(this.changeText);
  }
}
