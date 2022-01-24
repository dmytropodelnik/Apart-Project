import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  public isCollapsed = true;
  email: string | undefined;

  constructor() { }

  fetchRequest() {
    fetch('https://localhost:44381/api/deals/sendbestdealsemail?email=' + this.email, {
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
