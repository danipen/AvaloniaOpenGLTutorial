namespace Tutorial22
{
    internal interface IModel
    {
        internal uint[] Indices { get; }
        internal Vertex[] Vertices { get;}

        void LoadMesh();
    }
}