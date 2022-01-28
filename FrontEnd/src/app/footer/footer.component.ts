import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  public isCollapsed = true;
  email: string | undefined;
  registerForm: FormGroup;
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$"; 

  constructor(private formBuilder: FormBuilder) {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email,Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
    });
   }
   get f() { return this.registerForm.controls; }
  fetchRequest() {
    fetch('https://localhost:44381/api/deals/sendbestdealsletter?email=' + this.email, {
      method: 'GET',
    })
    .then(r => r.json())
    .then(r => {
      if (r.code === 200) {
        alert("A letter has been successfully sent to your email address!");
      } else {
        alert("Error!");
      }
    })
    .catch(err => {
      alert(err);
    });
  }

  ngOnInit(): void {
  }

}
