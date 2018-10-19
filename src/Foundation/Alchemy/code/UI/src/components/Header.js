import React from 'react'
import ParcelLogo from "../img/rocket-alchemy.svg";

const Header = () => (
    <header>
        <nav className="navbar" role="navigation" aria-label="main navigation">
            <div className="navbar-brand">
                <a className="navbar-item" href="/">
                    <img className="logo" width="60" src={ParcelLogo} alt=""/><h1>Project Alchemy</h1>
                </a>
            </div>
        </nav>
    </header>
)

export default Header