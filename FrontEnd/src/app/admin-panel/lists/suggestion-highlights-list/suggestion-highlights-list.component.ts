import { Component, OnInit } from '@angular/core';
import { SuggestionHighlight } from 'src/app/models/Suggestions/suggestionhighlight.item';

@Component({
  selector: 'app-suggestion-highlights-list',
  templateUrl: './suggestion-highlights-list.component.html',
  styleUrls: ['./suggestion-highlights-list.component.css']
})
export class SuggestionHighlightsListComponent implements OnInit {

  highlights: SuggestionHighlight[] | null = null;

  constructor() { }

  ngOnInit(): void {
    fetch('https://localhost:44381/api/countries/getcountries', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.highlights = data.highlights;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

}
