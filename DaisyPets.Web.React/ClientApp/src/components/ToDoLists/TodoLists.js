import React, { useState, useEffect } from 'react';

const ToDoLists = () => {
    const [todos, setTodos] = useState([]);
    const todosEndpoint = 'https://localhost:7161/api/ToDos';
    useEffect(() => {
        fetch(todosEndpoint)
            .then((results) => {
                return results.json();
            })
            .then(data => {
                setTodos(data);
            })
    }, []);

    return (
        (todos.length > 0) ?
        <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th width="60%">Description</th>
                    <th width="30%">Category</th>
                    <th width="10%">Start Date</th>
                </tr>
            </thead>
            <tbody>
                {todos.map(item =>
                    <tr key={item.description}>
                        <td width="60%">{item.description}</td>
                        <td width="30%">{item.categoryDescription}</td>
                        <td width="10%">{item.startDate}</td>
                    </tr>
                )}
            </tbody>
        </table> :  <h3>Loading...</h3>
    );
}

export default ToDoLists;
