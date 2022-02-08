import { Component, OnInit } from '@angular/core';
import { UserProfile } from 'src/app/models/UserData/useprofile.item';

@Component({
  selector: 'app-user-profiles-list',
  templateUrl: './user-profiles-list.component.html',
  styleUrls: ['./user-profiles-list.component.css']
})
export class UserProfilesListComponent implements OnInit {

  profiles: UserProfile[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.profiles = data.profiles;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
