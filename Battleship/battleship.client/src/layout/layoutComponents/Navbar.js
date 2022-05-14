import { Menu } from "semantic-ui-react";
import { NavLink } from "react-router-dom";

export default function Navbar() {
    return (
      <div id="header">
      <Menu icon="labeled" >
        <Menu.Item name="home" as={NavLink} exact to="/" >
          <img src="assets\images\battleship-silhouette-8.png" alt="logo" />
        </Menu.Item>
        <Menu.Item name="game" icon="game" as={NavLink} to="/Game"/>
        <Menu.Item name="Test" icon="Test" as={NavLink} to="/Test"/>
        <Menu.Menu secondary position="right">
          <Menu.Item name="login" icon="user outline" />
        </Menu.Menu>
      </Menu>
      </div>
    );
}