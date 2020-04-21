import React, { Component } from 'react';
import { NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Chess demo</h1>
        <h4>This is simple application which demonstrates chess figure movement rudiments</h4>        
            <h4>
            <NavLink tag={Link} className="text-dark" to="/chess-game">Lets start!</NavLink>              
            </h4>
       
      </div>
    );
  }
}
