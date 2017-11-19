namespace CoreExtensions
{
    internal class Result
    {
        private readonly string searched;
        private readonly string result;
        private readonly double score;

        public Result(string searched, string result, double score)
        {
            this.searched = searched;
            this.result = result;
            this.score = score;
        }

        public string Searched => searched;

        public string TheResult => result;

        public double Score => score;

        public override string ToString()
        {
            return string.Format("Searched: {0}, Result: {1}, Score: {2}", searched, result, score);
        }

        protected bool Equals(Result other)
        {
            return string.Equals(searched, other.searched) && string.Equals(result, other.result) && score.Equals(other.score);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Result) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (searched != null ? searched.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (result != null ? result.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ score.GetHashCode();
                return hashCode;
            }
        }
    }
}
