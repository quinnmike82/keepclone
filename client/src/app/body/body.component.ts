import { SocialAuthService } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Note } from 'src/_models/note';
import { User } from 'src/_models/user';
import { BehaviorSubject, timer } from 'rxjs';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.css']
})
export class BodyComponent {
  inputNote = false
  note : any = {}
 

  title = 'angular-google';
  filePath : any

  model:any = {}
  user:any;
  loggedIn:any;

  notes : any = []


  constructor(private modalService: NgbModal,private authService: SocialAuthService, private http : HttpClient) {
   }

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
    this.http.get<Note>('https://localhost:5001/api/note/userid/'+this.user.id).subscribe({
      next : response => {
        this.notes = response
        console.log(response)
      },
      error : error => console.log(error.error)
    })
  }

  changeText : any = {}

  change(){
    console.log(this.notes);
  }

  addNote(){
    const formData = new FormData()
    formData.append('file', this.filePath) 
    formData.append('title', this.note.title)
    formData.append('colorBG', this.note.colorBG)
    formData.append('timerSet', this.note.timerSet)
    formData.append('content', this.note.content)
    formData.append('userId', this.user.id)

    this.http.post('https://localhost:5001/api/note/addnote/',formData).subscribe({
      next : response => console.log(response)
    })
  }

  onFileSelected(event : any){
    this.filePath = event.target.files[0]
    const fileSizeInMB = this.filePath.size / (1024 * 1024); // Convert to MB
  if (fileSizeInMB <= 2) {
    // File is less than or equal to 2MB
    console.log('File size:', fileSizeInMB);
  } else {
    // File is greater than 2MB
    console.log('File size is too large:', fileSizeInMB);
    event.target.file = null
    this.filePath = null
  }
  }

  changeInputNote(event : any){
    this.inputNote = true
  }

  currentNote : any
  closeResult = ''

  open(content : any, i : any) {
    this.currentNote = this.notes[i]
    console.log(this.currentNote);
		this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
			(result) => {
				this.closeResult = `Closed with: ${result}`;
        console.log(this.closeResult);
			},
			(reason) => {
				this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;

			},
		);
    
	}
  private getDismissReason(reason: any): string {
		if (reason === ModalDismissReasons.ESC) {
			return 'by pressing ESC';
		} else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
			return 'by clicking on a backdrop';
		} else {
			return `with: ${reason}`;
		}
	}
  updateNote(){
    this.http.put<Note>('https://localhost:5001/api/note/updatenote/',this.currentNote).subscribe({
      next : response =>{
        console.log(response)
      } ,
      error : error => console.log(error.error)
    })
  }
}
