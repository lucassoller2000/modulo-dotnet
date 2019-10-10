import React from 'react'
import './NotFound.css'
import { Jumbotron } from 'reactstrap'

export default class NotFound extends React.Component {

    render() {
        return <div className="error">
            <Jumbotron className="jumbo">
                <h1 className="display-3">Error 404 Page Not Found</h1>
            </Jumbotron>
        </div>
    }

}